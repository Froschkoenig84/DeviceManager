import { Injectable } from '@angular/core';
import { Observable, of, timer, BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class HealthService {
  private isOnline$ = new BehaviorSubject<boolean>(true);

  startMonitoring() {}

  setOnlineStatus(status: boolean) {
    this.isOnline$.next(status);
  }

  get status$() {
    return this.isOnline$.asObservable();
  }
}
