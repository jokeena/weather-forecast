import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { getWeather, type WeatherForecast } from './services/WeatherService';

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
  useEffect is run last. The useState is run first, and initializes weather.
  Then, the return is exectued, which returns an empty ul list with no li elements, because weather is []. 
  After the return, useEffect is triggered, updating the ul with the data from the HTTP GET and the updated weather state.
  */
  useEffect(() => { 
    //getWeather is called, we trigger an HTTP GET request to API, when that returns we update the weather State using setWeather.
    getWeather().then(setWeather).catch(console.error);

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
      /* 
      This component is rendered twice. The intial render happens before the useEffect is triggered. Thus an empty ul is returned, because weather = [].
      After we return the empty <ul></ul>, useEffect is triggered and the state of weather is updated. now weather = [...weatherData].
      Thus on the second render we return a <ul><li>20C</li><li><40C></li>...</ul> with 5 li elements. 
      */
      <>
      <div className="weather-list" style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', gap: '1rem' }}>
        <h1 style={{ margin: 0 }}>Weather Forecast</h1>
        <h2 style={{ margin: 0, paddingTop: 20 }}>{'\uD83D\uDCCD'}{userLocation}</h2>
      </div>
        <div className="weather-container">
          {weather.map(item => ( // weather.map(item => ( iterates over each weather item. React requires an unique key for list items (item.date). Every item's (WeatherForecast object) temperature is displayed
            <div key={item.date} className="weather-card">
              <div className="weather-date">{item.formattedDate}</div>
              <div className="weather-temp" style={{ color: item.temperatureColor }}>
                {item.temperatureF}{'\u00B0'}F
              </div>
              <div className="weather-summary" style={{ color: item.temperatureColor }}>
                {item.summary}
              </div>
              <div className="weather-emoji">{item.summaryEmoji}</div>
            </div>
          ))}
        </div>
      </>
    )
}

export default App
