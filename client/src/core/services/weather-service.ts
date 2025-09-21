import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherForecast } from '../../types/weather';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private http = inject(HttpClient);

  getWeather(location: string): Observable<WeatherForecast> {
    return this.http.get<WeatherForecast>(`${environment.appUrl}?location=${location}`);
  }
}
