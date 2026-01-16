import { Routes } from '@angular/router';
import { DeviceOverviewComponent } from './pages/device-overview/device-overview.component';
import { DeviceDetailsComponent } from './pages/device-details/device-details.component';

export const routes: Routes = [
  // 1. Startseite direkt auf die Übersicht leiten
  { path: '', component: DeviceOverviewComponent },

  // 2. Detailseite mit der GUID als Parameter
  { path: 'device/:id', component: DeviceDetailsComponent },

  // 3. Fallback (falls sich wer vertippt)
  { path: '**', redirectTo: '' }
];
