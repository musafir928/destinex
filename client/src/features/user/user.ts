import { Component, inject, signal } from '@angular/core';
import { WeatherForecast } from '../../types/weather';
import { WeatherService } from '../../core/services/weather-service';
import { ToastService } from '../../core/services/toast-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.html',
  styleUrl: './user.css',
  imports: [FormsModule],
})
export class User {
  private weatherService = inject(WeatherService);
  private toast = inject(ToastService);

  location: string = '';
  weather = signal<WeatherForecast | null>(null);
  loading = signal(false);
  error = signal<string | null>(null);

  searchWeather() {
    const loc = this.location.trim();
    if (!loc) {
      this.toast.error('Please enter a location name');
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.weatherService.getWeather(loc).subscribe({
      next: (data) => {
        this.weather.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.toast.error('Could not fetch weather. Please try again.');
        this.error.set('Could not fetch weather. Please try again.');
        this.loading.set(false);
      },
    });
  }
}
