import { Routes } from '@angular/router';
import { DeviceOverviewComponent } from './pages/device-overview/device-overview.component';
import { DeviceDetailsComponent } from './pages/device-details/device-details.component';

export const routes: Routes = [
  { path: '', component: DeviceOverviewComponent },
  { path: 'device/:guid', component: DeviceDetailsComponent },
  { path: '**', redirectTo: '' }
];
