import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DeviceDetailsComponent } from './device-details.component';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { provideHttpClientTesting } from '@angular/common/http/testing';

describe('DeviceDetailsComponent', () => {
  let component: DeviceDetailsComponent;
  let fixture: ComponentFixture<DeviceDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      // Da Standalone, gehört die Component in imports
      imports: [DeviceDetailsComponent],
      // Das hier hat gefehlt: Die Infrastruktur für Service & Routing
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([])
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(DeviceDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges(); // Statt whenStable, um den Initialisierungs-Zyklus zu triggern
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
