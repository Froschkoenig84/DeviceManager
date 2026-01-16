import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { DeviceService } from '../../services/device.service';
import { Device } from '../../models/device.model';

@Component({
  selector: 'app-device-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  // WICHTIG: Die Dateinamen müssen stimmen!
  templateUrl: './device-details.component.html',
  styleUrl: './device-details.component.scss',
})
export class DeviceDetailsComponent implements OnInit { // Name korrigiert!
  device?: Device;

  constructor(
    private route: ActivatedRoute,
    private deviceService: DeviceService
  ) {}

  ngOnInit(): void {
    const guid = this.route.snapshot.paramMap.get('id');
    if (guid) {
      this.deviceService.getDetailedDeviceByGuid(guid).subscribe(data => {
        this.device = data;
      });
    }
  }
}
