import axios from 'axios';

import { CURRENT_USER, INTERNAL_API_KEY } from '@/lib/server/secrets';

export const http = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json',
    'X-Api-Key': INTERNAL_API_KEY,
    'X-User-Id': CURRENT_USER.id,
    'X-User-Display-Name': CURRENT_USER.displayName,
    'X-Roles': CURRENT_USER.roles.join(','),
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
