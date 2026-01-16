import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Device } from '../models/device.model';

@Injectable({ providedIn: 'root' })
export class DeviceService {
  private apiUrl = 'http://localhost:5280/api/devices';

  constructor(private http: HttpClient) {}

  // [GET] devices/overview
  getOverviewDevices(): Observable<Device[]> {
    return this.http.get<Device[]>(`${this.apiUrl}/overview`);
  }

  // [GET] devices/{guid}/detailed
  getDetailedDeviceByGuid(guid: string): Observable<Device> {
    return this.http.get<Device>(`${this.apiUrl}/${guid}/detailed`);
  }

  // [POST] devices
  createDevices(payload: any[]): Observable<any> {
    return this.http.post<any>(this.apiUrl, payload);
  }

  // [DELETE] devices/{guid}
  deleteDeviceByGuid(guid: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${guid}`);
  }
}
