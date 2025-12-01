import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000';

const api = axios.create({
  baseURL: `${API_URL}/api`,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const authService = {
  login: (username: string, password: string) =>
    api.post('/Auth/login', { username, password }),
};

export const productService = {
  getProducts: (page: number, size: number, search?: string) =>
    api.get(`/Product?page=${page}&size=${size}${search ? `&search=${search}` : ''}`),
  getProduct: (id: string) => api.get(`/Product/${id}`),
  createProduct: (product: any) => api.post('/Product', product),
  updateProduct: (id: string, product: any) => api.put(`/Product/${id}`, product),
  deleteProduct: (id: string) => api.delete(`/Product/${id}`),
  generateProducts: (count: number) => api.post(`/Product?count=${count}`),
};

export const categoryService = {
  getCategories: () => api.get('/Category'),
  createCategory: (category: any) => api.post('/Category', category),
};

export default api;