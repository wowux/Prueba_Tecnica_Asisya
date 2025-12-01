import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './components/Login';
import ProductList from './components/ProductList';
import AuthGuard from './components/AuthGuard';

const App: React.FC = () => {
  const handleLogout = () => {
    localStorage.removeItem('token');
    window.location.href = '/login';
  };

  return (
    <Router>
      <div>
        <nav style={{ padding: '10px', backgroundColor: '#f8f9fa', borderBottom: '1px solid #ddd' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <h1>Asisya Products</h1>
            {localStorage.getItem('token') && (
              <button onClick={handleLogout} style={{ padding: '8px 16px' }}>
                Logout
              </button>
            )}
          </div>
        </nav>
        
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/products" element={
            <AuthGuard>
              <ProductList />
            </AuthGuard>
          } />
          <Route path="/" element={<Navigate to="/products" replace />} />
        </Routes>
      </div>
    </Router>
  );
};

createRoot(document.getElementById('root')!).render(<App />);