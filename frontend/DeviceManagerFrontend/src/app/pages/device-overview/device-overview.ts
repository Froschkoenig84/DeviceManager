import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // Den hast du schon, brav!

@Component({
  selector: 'app-device-overview',
  standalone: true, // Sicherstellen, dass das hier steht
  imports: [CommonModule], // Hier muss das CommonModule rein!
  templateUrl: './device-overview.html',
  styleUrls: ['./device-overview.scss'],
})
export class DeviceOverview { }
