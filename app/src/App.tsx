import { Routes, Route } from 'react-router-dom';
import Layout from './Layout';
import Profiles from './views/Profiles';
import ProfileRepositories from './views/ProfileRepositories';
import { CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import { useEffect } from 'react';
import ProfileRepositoriesDetails from './views/ProfileRepositoriesDetails';

function App() {
  const darkTheme = createTheme({
    palette: {
      mode: 'dark'
    }
  });

  return (
    <ThemeProvider theme={darkTheme}>
      <CssBaseline />
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Profiles />} />
          <Route path="/profiles/:id" element={<ProfileRepositories />} />
          <Route path="/profiles/:id/:repoName" element={<ProfileRepositoriesDetails />} />
          {/* Using path="*"" means "match anything", so this route
            acts like a catch-all for URLs that we don't have explicit
            routes for. 
            <Route path="*" element={<NoMatch />} />
            */}
        </Route>
      </Routes>
    </ThemeProvider>
  );
}

export default App;
