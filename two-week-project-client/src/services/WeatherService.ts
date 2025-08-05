import axios from 'axios';

// Url for backend route in WeatherForecastController.cs
const API_BASE_URL = 'https://localhost:7298/weatherforecast'; // Adjust if needed

// creates an interface for WeatherForecast that shows the structure of what we declared in WeatherForecast.cs
export interface WeatherForecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
    formattedDate : string;
    summaryEmoji : string;
    temperatureColor: string;
    activities: string[];
    clothing: string[];
}

// export makes it available to other files (as a constant). Asynchronous function that returns a promise that resolves to an array of WeatherForecast objects.
export const getWeather = async (): Promise<WeatherForecast[]> => {
  const response = await axios.get(API_BASE_URL); // use axios to trigger HTTP GET request to API
  return response.data; // Returns data portion of HTTP response to App.tsx for rendering.
};
