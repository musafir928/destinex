export interface WeatherForecast {
  location: string;
  region: string;
  country: string;
  currentC: number;
  todayMinC: number;
  todayMaxC: number;
  tomorrowMinC: number;
  tomorrowMaxC: number;
  afterTomorrowMinC: number;
  afterTomorrowMaxC: number;
}
