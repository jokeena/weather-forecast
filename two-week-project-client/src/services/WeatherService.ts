import axios from 'axios';

// Base URL for all API endpoints
const BASE_URL = 'https://localhost:7298';

// API endpoints
const ENDPOINTS = {
  weatherForecast: `${BASE_URL}/weatherforecast`,
  weatherMap: `${BASE_URL}/weathermap`,
  // Future endpoints can be added here
  // weatherAlerts: `${BASE_URL}/weatheralerts`,
} as const;

// creates an interface for WeatherForecast that shows the structure of what we declared in WeatherForecast.cs
export interface WeatherForecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
    formattedDate : string;
    summaryEmoji : string;
    weatherEmoji : string;
    temperatureColor: string;
    activities: string[];
    clothing: string[];
    weatherIconDescriptor: string;
    weatherIconClass: string;
    weatherIconColor: string;
}

// export makes it available to other files (as a constant). Asynchronous function that returns a promise that resolves to an array of WeatherForecast objects.
export const getWeather = async (): Promise<WeatherForecast[]> => {
  const response = await axios.get(ENDPOINTS.weatherForecast); // use axios to trigger HTTP GET request to API
  return response.data; // Returns data portion of HTTP response to App.tsx for rendering.
};

// Function to get weather map data
export const getWeatherMap = async (): Promise<any> => { // You'd define a proper interface for map data
  const response = await axios.get(ENDPOINTS.weatherMap);
  return response.data;
};
