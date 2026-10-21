import axios from 'axios';



export const http = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

http.interceptors.request.use((config) => {
  const correlationId =
    typeof crypto !== 'undefined' && crypto.randomUUID
      ? crypto.randomUUID()
      : `corr-${Date.now()}`;
  config.headers['X-Correlation-Id'] = correlationId;
  return config;
});
