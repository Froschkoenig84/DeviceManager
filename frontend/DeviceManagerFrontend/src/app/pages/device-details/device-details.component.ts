import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import {ActivatedRoute, RouterLink} from '@angular/router';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-device-details',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './device-details.component.html',
  styleUrls: ['./device-details.component.scss']
})
export class DeviceDetailsComponent implements OnInit {
  device: Device | null = null;

  constructor(
    private route: ActivatedRoute,
    private deviceService: DeviceService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    console.log('--- Detailansicht gestartet ---');

    // Versuch A: Aus dem Snapshot (statisch)
    let guid = this.route.snapshot.paramMap.get('guid');
    if (guid) this.loadDeviceDetails(guid);
  }

  loadDeviceDetails(guid: string): void {
    this.deviceService.getDetailedDeviceByGuid(guid).subscribe({
      next: (data: Device) => {
        this.device = data;
        this.cdr.detectChanges(); // Wach auf, Angular!
        console.log('Device im State gesetzt:', this.device);
      },
      error: (err: any) => console.error('Fehler:', err)
    });
  }
}
