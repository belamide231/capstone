import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ZigzagChartComponent } from './zigzag-chart.component';

describe('ZigzagChartComponent', () => {
  let component: ZigzagChartComponent;
  let fixture: ComponentFixture<ZigzagChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ZigzagChartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ZigzagChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
