import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DeviceOverviewComponent } from './device-overview.component';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';

describe('DeviceOverviewComponent', () => {
  let component: DeviceOverviewComponent;
  let fixture: ComponentFixture<DeviceOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      // Da es eine Standalone Component ist, kommt sie in imports
      imports: [DeviceOverviewComponent],
      providers: [
        provideHttpClient(),
        provideRouter([])
      ]
    })
      .compileComponents();

    fixture = TestBed.createComponent(DeviceOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
