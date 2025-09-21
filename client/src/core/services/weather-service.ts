import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherForecast } from '../../types/weather';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private http = inject(HttpClient);
  private apiUrl = '/api/weather/forecast';

  getWeather(location: string): Observable<WeatherForecast> {
    return this.http.get<WeatherForecast>(`${this.apiUrl}?location=${location}`);
  }
}
