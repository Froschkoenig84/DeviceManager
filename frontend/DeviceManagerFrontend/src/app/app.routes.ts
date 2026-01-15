import { Routes } from '@angular/router';
import { DeviceOverview } from './pages/device-overview/device-overview';
import { DeviceDetail } from './pages/device-detail/device-detail';

export const routes: Routes = [ // Das EXPORT ist entscheidend!
  { path: '', component: DeviceOverview },
  { path: 'device/:metaId', component: DeviceDetail },
  { path: '**', redirectTo: '' }
];
