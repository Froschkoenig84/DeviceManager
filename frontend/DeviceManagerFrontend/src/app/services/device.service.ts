import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// DTO für Geräteübersicht
export interface DeviceOverviewDto {
  metaId: string;       // interne GUID vom Backend
  name: string;
  failsafe: boolean;
  deviceTypeId: string;
}

// DTO für Detailansicht
export interface DeviceDetailDto extends DeviceOverviewDto {
  tempMin: number;
  tempMax: number;
  installationPosition: string;
  insertInto19InchCabinet: boolean;
  terminalElement?: boolean;
  advancedEnvironmentalConditions?: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private baseUrl = 'http://localhost:5280/api/devices';

  constructor(private http: HttpClient) {}

  // Alle Geräte für Übersicht
  getAllOverviewDevices(): Observable<DeviceOverviewDto[]> {
    return this.http.get<DeviceOverviewDto[]>(`${this.baseUrl}/overview`);
  }

  // Gerätedetails für Detailansicht
  getDetailedDevice(metaId: string): Observable<DeviceDetailDto> {
    return this.http.get<DeviceDetailDto>(`${this.baseUrl}/${metaId}`);
  }

  // Gerät löschen
  deleteDevice(metaId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${metaId}`);
  }

  // Daten an Backend senden
  createDevices(devices: DeviceOverviewDto[]): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, devices);
  }

  // Lokale Datei vom Client einlesen, validieren und dann createDevices() aufrufen
  uploadDevices(file: File): Observable<void> {
    return new Observable<void>(observer => {
      const reader = new FileReader();
      reader.onload = () => {
        try {
          const json = JSON.parse(reader.result as string);
          if (!json.devices || !Array.isArray(json.devices)) {
            throw new Error('Ungültige Datei: "devices" fehlt oder ist kein Array');
          }
          this.createDevices(json.devices).subscribe({
            next: () => {
              observer.next();
              observer.complete();
            },
            error: err => observer.error(err)
          });
        } catch (err) {
          observer.error(err);
        }
      };
      reader.onerror = err => observer.error(err);
      reader.readAsText(file);
    });
  }
}
