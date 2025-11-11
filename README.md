# Weather Forecast Application

A full-stack weather forecasting application that provides a 2-week weather forecast with an interactive map and location-based weather information.

## 📋 Project Overview

This is a full-stack application consisting of:
- **Frontend**: React + TypeScript + Vite
- **Backend**: ASP.NET Core with C#
- **APIs**: Real weather data integration with interactive mapping

The application displays a 2-week weather forecast for the user's current location, complete with temperature data, weather summaries, recommended activities, and clothing suggestions.

## 🏗️ Architecture

### Frontend (`two-week-project-client/`)

A modern React application built with:
- **Framework**: React 19 with TypeScript
- **Build Tool**: Vite
- **Styling**: CSS with Font Awesome icons
- **Routing**: React Router v7 for navigation
- **HTTP Client**: Axios for API communication
- **Geolocation**: Browser Geolocation API to get user location
- **Reverse Geocoding**: BigDataCloud API to convert coordinates to city/state

#### Key Components:
- **App.tsx**: Main application component with routing and location detection
- **WeatherCards**: Displays weather forecast cards for each day
- **WeatherMap**: Interactive weather map view

#### Features:
- Bottom navigation bar for easy switching between forecast and map views
- Automatic user location detection and display
- Responsive weather card layout
- Icon-based weather visualization

### Backend (`TwoWeekProject/`)

An ASP.NET Core REST API that:
- Fetches real weather data from external sources
- Processes and enriches weather data with:
  - Temperature conversions (Celsius to Fahrenheit)
  - Weather emojis and icons
  - Temperature color coding
  - Recommended activities based on weather
  - Clothing suggestions
- Provides endpoints for the frontend to consume

#### Key Components:
- **WeatherForecastController**: Main API endpoint for forecast data
- **WeatherMapController**: Endpoint for map visualization
- **RealWeatherDataController**: Handles real weather data fetching
- **WeatherForecast.cs**: Model for weather data
- **RealWeatherData.cs**: Model for external weather API responses

#### API Endpoints:
- `GET /weatherforecast` - Returns 2-week weather forecast
- `GET /weathermap` - Returns weather map data

## 🚀 Getting Started

### Prerequisites

- **Node.js** (v16 or higher)
- **.NET 8 SDK** or higher
- **npm** or **yarn** for Node package management

### Frontend Setup

1. Navigate to the client directory:
```bash
cd two-week-project-client
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm run dev
```

The frontend will be available at `http://localhost:5173`

### Backend Setup

1. Navigate to the backend directory:
```bash
cd TwoWeekProject/TwoWeekProject
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

The backend API will be available at `https://localhost:7298`

## 📦 Available Scripts

### Frontend

- `npm run dev` - Start Vite development server
- `npm run build` - Build for production with TypeScript checking
- `npm run lint` - Run ESLint to check code quality
- `npm run preview` - Preview production build locally

### Backend

- `dotnet run` - Run the application
- `dotnet build` - Build the project
- `dotnet watch run` - Run with file watching for development

## 🔧 Configuration

### Frontend

- **Vite Config**: `vite.config.ts`
- **TypeScript Config**: `tsconfig.json`
- **ESLint Config**: `eslint.config.js`

### Backend

- **App Settings**: `appsettings.json` (production) and `appsettings.Development.json` (development)
- **Project File**: `TwoWeekProject.csproj`
- **Launch Settings**: `Properties/launchSettings.json`

## 🌐 CORS Configuration

The backend is configured to accept requests from the frontend development server at `http://localhost:5173`. This is defined in `Program.cs` under the "AllowViteClient" CORS policy.

## 🗺️ Data Flow

1. User opens the application
2. Frontend detects user's location using browser geolocation
3. Frontend converts coordinates to city/state using BigDataCloud API
4. Frontend displays location in header
5. Frontend fetches weather forecast from backend API
6. Backend retrieves real weather data and enriches it with:
   - Formatted dates
   - Emojis and icons
   - Temperature colors
   - Activity recommendations
   - Clothing suggestions
7. Frontend renders weather cards with all information
8. User can switch between forecast view and map view using bottom navigation

## 🎨 Features

- ✅ Real-time 2-week weather forecast
- ✅ Automatic user location detection
- ✅ Color-coded temperature display
- ✅ Weather-based activity recommendations
- ✅ Clothing suggestions based on weather
- ✅ Interactive weather icons
- ✅ Weather map visualization
- ✅ Responsive design
- ✅ Bottom navigation for easy navigation

## 📱 Responsive Design

The application is designed to work on both desktop and mobile devices with a bottom navigation bar for easy thumb access on mobile devices.

## 🐛 Troubleshooting

### Frontend won't connect to backend
- Ensure the backend is running on `https://localhost:7298`
- Check that CORS is properly configured in `Program.cs`
- Verify the `BASE_URL` in `WeatherService.ts` matches your backend URL

### Location not detected
- Ensure you've granted the browser permission to access your location
- Check browser console for any geolocation errors
- BigDataCloud API requires an internet connection

### Backend won't start
- Ensure .NET 8 SDK or higher is installed
- Check that port 7298 is not in use
- Verify all NuGet packages are restored with `dotnet restore`

## 📝 Development Notes

- The frontend uses TypeScript for type safety
- React hooks are used for state management (useState, useEffect)
- CORS is enabled for development convenience
- The backend provides comprehensive weather enrichment
- Real weather data is integrated from external sources

## 📄 License

This project is part of a two-week development project.

## 👥 Authors

- Frontend & Backend Development Team

---

Happy forecasting! 🌤️
