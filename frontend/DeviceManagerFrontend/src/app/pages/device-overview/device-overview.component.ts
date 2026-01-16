import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common'; // Für *ngIf und *ngFor
import { RouterModule } from '@angular/router'; // Für [routerLink]
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device.model';

@Component({
  selector: 'app-device-overview',
  standalone: true, // Sicherstellen, dass das hier steht!
  imports: [
    CommonModule, // Schaltet *ngIf und *ngFor frei
    RouterModule  // Schaltet [routerLink] frei
  ],
  templateUrl: './device-overview.component.html',
  styleUrls: ['./device-overview.component.scss']
})
export class DeviceOverviewComponent implements OnInit {
  devices: Device[] = [];

  constructor(
    private deviceService: DeviceService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    console.log('Komponente initialisiert, lade Daten...');
    this.refreshOverview();
  }

  refreshOverview(): void {
    this.deviceService.getOverviewDevices().subscribe({
      next: (data) => {
        this.devices = data;
        this.cdr.detectChanges();
        console.log('UI Refresh erzwungen mit Daten:', data);
      },
      error: (err) => console.error('Abruf fehlgeschlagen', err)
    });
  }

  handleJsonUpload(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const file = input.files[0];
    const reader = new FileReader();

    reader.onload = (e) => {
      try {
        const rawContent = e.target?.result as string;
        const jsonContent = JSON.parse(rawContent);

        const payload = Array.isArray(jsonContent) ? jsonContent : jsonContent.devices;

        if (!payload) {
          throw new Error("Kein 'devices'-Array im JSON gefunden!");
        }

        this.deviceService.createDevices(payload).subscribe({
          next: () => {
            this.refreshOverview();
            input.value = '';
          },
          error: (err) => {
            console.error('Upload fehlgeschlagen:', err);
            alert('Backend meldet Fehler. Evtl. GUID-Konflikt oder Format falsch?');
          }
        });
      } catch (error) {
        alert('Fehler beim Verarbeiten der Datei: ' + error);
      }
    };

    reader.readAsText(file);
  }

  removeDevice(guid: string, event: Event): void {
    event.stopPropagation();
    if (confirm('Gerät wirklich löschen?')) {
      this.deviceService.deleteDeviceByGuid(guid).subscribe(() => this.refreshOverview());
    }
  }
}
