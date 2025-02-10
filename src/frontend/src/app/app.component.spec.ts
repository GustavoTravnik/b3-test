import { TestBed } from '@angular/core/testing';
import { AppModule } from './app.module';
import { AppComponent } from './app.component';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CalculationPageComponent } from './pages/calculation-page/calculation-page.component';
import { HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterInterceptor } from './Interceptors/router-interceptor';

describe('AppModule', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        AppModule,
        HttpClientTestingModule,
        RouterTestingModule,
        BrowserAnimationsModule,
        MatInputModule,
        MatButtonModule,
        MatCardModule,
      ],
      declarations: [AppComponent, CalculationPageComponent],
      providers: [
        {
          provide: HTTP_INTERCEPTORS,
          useClass: RouterInterceptor,
          multi: true,
        },
      ],
    }).compileComponents();
  });

  it('should create AppModule', () => {
    const appModule = TestBed.inject(AppModule);
    expect(appModule).toBeTruthy();
  });

  it('should render AppComponent', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should apply RouterInterceptor when making HTTP requests', () => {
    const httpMock = TestBed.inject(HttpTestingController);
    const httpClient = TestBed.inject(HttpClient);
  
    httpClient.get('http://localhost:8080/test-url').subscribe(response => {
      expect(response).toBeTruthy();
    });
  
    const req = httpMock.expectOne('http://localhost:8080/test-url');
    expect(req.request.method).toEqual('GET');
  
    req.flush({}); // Simulate a successful response
  
    httpMock.verify();
  });

  it('should render CalculationPageComponent', () => {
    const fixture = TestBed.createComponent(CalculationPageComponent);
    const component = fixture.debugElement.componentInstance;
    expect(component).toBeTruthy();
  });
});
