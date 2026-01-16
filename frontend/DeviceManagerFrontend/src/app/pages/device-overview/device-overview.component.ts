import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HealthService } from '../../services/health.service';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device.model';
import { Observable, combineLatest, map } from 'rxjs';

@Component({
  selector: 'app-device-overview',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './device-overview.component.html',
  styleUrls: ['./device-overview.component.scss']
})
export class DeviceOverviewComponent implements OnInit {
  devices: Device[] = [];
  isApiAvailable: boolean = true;

  constructor(
    private deviceService: DeviceService,
    private healthService: HealthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.healthService.status$.subscribe(status => {
      this.isApiAvailable = status;
      this.cdr.detectChanges();
    });
    this.refreshOverview();
  }

  refreshOverview(): void {
    this.deviceService.getOverviewDevices().subscribe({
      next: (data) => {
        this.devices = data;
        this.isApiAvailable = true;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error(err);
        this.isApiAvailable = false;
        this.cdr.detectChanges();
      }
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

        if (!payload) throw new Error("No devices array found");

        this.deviceService.createDevices(payload).subscribe({
          next: () => {
            this.refreshOverview();
            input.value = '';
          },
          error: () => {
            alert('Upload fehlgeschlagen.');
          }
        });
      } catch (error) {
        alert('Fehler: ' + error);
      }
    };
    reader.readAsText(file);
  }

  removeDevice(guid: string, event: Event): void {
    event.stopPropagation();
    if (confirm('Gerät wirklich löschen?')) {
      this.deviceService.deleteDeviceByGuid(guid).subscribe({
        next: () => this.refreshOverview(),
        error: () => {
          this.isApiAvailable = false;
          this.cdr.detectChanges();
        }
      });
    }
  }
}
