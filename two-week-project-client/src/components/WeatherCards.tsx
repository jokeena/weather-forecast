import { useState } from 'react';
import { type WeatherForecast } from '../services/WeatherService';
import './WeatherCards.css';
import '@fortawesome/fontawesome-free/css/all.min.css';

interface WeatherCardsProps {
  weather: WeatherForecast[];
}

function WeatherCards({ weather }: WeatherCardsProps) {
  // State to track which cards are flipped (using date as key)
  const [flippedCards, setFlippedCards] = useState<Set<string>>(new Set());

  // Function to toggle card flip
  const toggleCardFlip = (date: string) => {
    setFlippedCards(prev => {
      const newSet = new Set(prev);
      if (newSet.has(date)) {
        newSet.delete(date);
      } else {
        newSet.add(date);
      }
      return newSet;
    });
  };

  return (
    <div className="weather-container">
      {weather.map(item => {
        const isFlipped = flippedCards.has(item.date);
        
        return (
          <div key={item.date} className={`weather-card ${isFlipped ? 'flipped' : ''}`} onClick={() => toggleCardFlip(item.date)}>
            <div className="card-inner">
              {/* Front of the card */}
              <div className="card-front">
                <div className="weather-date">{item.formattedDate}</div>
                 <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', gap: '0.5rem' }}>
                <div className="weather-temp" style={{ color: item.temperatureColor }}>
                  {item.temperatureF}{'\u00B0'}F
                </div>
                <div className="summary-emoji">{item.summaryEmoji}</div>
                </div>
                  <div className="weather-summary" style={{ color: item.temperatureColor }}>
                    {item.summary}
                  </div>
                <div className="weather-icon">
                <i className={item.weatherIconClass} style={{ color: item.weatherIconColor, fontSize: 25 }}></i>
                </div>
                <div className="weather-descriptor">{item.weatherIconDescriptor}</div>
                <div className="flip-hint">Click to see activities!</div>
              </div>
              
              {/* Back of the card */}
              <div className="card-back">
                <div className="weather-date">{item.formattedDate}</div>
                
                <div className="activities-section">
                  <h4>🎯 Activities</h4>
                  <ul>
                    {item.activities.slice(0, 3).map((activity, index) => (
                      <li key={index}>{activity}</li>
                    ))}
                  </ul>
                </div>
                
                <div className="clothing-section">
                  <h4>👕 What to Wear</h4>
                  <ul>
                    {item.clothing.slice(0, 3).map((clothingItem, index) => (
                      <li key={index}>{clothingItem}</li>
                    ))}
                  </ul>
                </div>
              </div>
            </div>
          </div>
        );
      })}
    </div>
  );
}

export default WeatherCards;
