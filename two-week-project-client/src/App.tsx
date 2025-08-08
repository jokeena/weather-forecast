import { useEffect, useState } from 'react'
import { BrowserRouter as Router, Routes, Route, Link, useLocation } from 'react-router-dom'
import './App.css'
import { getWeather, type WeatherForecast } from './services/WeatherService';
import WeatherCards from './components/WeatherCards';
import WeatherMap from './components/WeatherMap';
import '@fortawesome/fontawesome-free/css/all.min.css';

// Bottom navigation bar component
function BottomNavigation() {
  const location = useLocation();
  
  return (
    <nav className="bottom-navigation">
      <Link 
        to="/" 
        className={`nav-button ${location.pathname === '/' ? 'active' : ''}`}
      >
        📊 Forecast
      </Link>
      
      <Link 
        to="/map" 
        className={`nav-button ${location.pathname === '/map' ? 'active' : ''}`}
      >
        🗺️ Weather Map
      </Link>
    </nav>
  );
}

function App() {
  /* 
  weather holds the current state value, intitial value []. can only modify weather through setWeather. 
  setWeather only takes WeatherForecast[] as input, which triggers a rerender of the component with the new state. 
  weather will always be an array of WeatherForecast objects and setWeather will only accept WeatherForecast[] as input, due to useState<WeatherForecast[]>. 
  */
  const [weather, setWeather] = useState<WeatherForecast[]>([])

  const [userLocation, setUserLocation] = useState<string>("");

  useEffect(() => {
    getUserLocation()
      .then(setUserLocation)
      .catch(error => {
        console.error('Location error:', error);
        setUserLocation("Location unavailable");
      });
  }, []);

  /*
  useEffect fetches weather forecast data and sets it to state.
  */
  useEffect(() => { 
    const fetchWeatherData = async () => {
      try {
        const forecastData = await getWeather();
        setWeather(forecastData);
      } catch (error) {
        console.error('Error fetching weather data:', error);
      }
    };

    fetchWeatherData();
  }, []); // Empty [] ensures this only runs once after initial render.

  const getUserLocation = async (): Promise<string> => {
    return new Promise((resolve, reject) => {
      navigator.geolocation.getCurrentPosition(
        async (position) => {
          try {
            const lat = position.coords.latitude;
            const lon = position.coords.longitude;
            
            const response = await fetch(
              `https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=${lat}&longitude=${lon}&localityLanguage=en`
            );
            const data = await response.json();
            
            const city = data.city || data.locality || "Unknown City";
            let state = data.principalSubdivisionCode || data.principalSubdivision || data.region || "";
            
            if (state.includes('-')) {
              state = state.split('-')[1];
            }
            
            const location = state ? `${city}, ${state}` : city;
            
            resolve(location);
          } catch (error) {
            reject(error);
          }
        },
        (error) => {
          reject(error);
        }
      );
    });
  };

  return (
    <Router>
      <div className="app-content">
        {/* Navigation Header */}
        <div className="weather-list app-header">
          <Link to="/" className="app-title-link">
            <h1 className="app-title">Weather App</h1>
          </Link>
          <h2 className="location-display">{'\uD83D\uDCCD'}{userLocation}</h2>
        </div>

        {/* Routes */}
        <Routes>
          <Route path="/" element={<WeatherCards weather={weather} />} />
          <Route path="/map" element={<WeatherMap />} />
        </Routes>
        
        {/* Fixed Bottom Navigation */}
        <BottomNavigation />
      </div>
    </Router>
  )
}

export default App
