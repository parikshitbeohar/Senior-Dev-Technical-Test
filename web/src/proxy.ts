import { NextRequest, NextResponse } from 'next/server';

import { CURRENT_USER, INTERNAL_API_KEY } from '@/lib/server/secrets';

export function proxy(request: NextRequest) {
  const headers = new Headers(request.headers);
  headers.set('X-Api-Key', INTERNAL_API_KEY);
  headers.set('X-User-Id', CURRENT_USER.id);
  headers.set('X-User-Display-Name', CURRENT_USER.displayName);
  headers.set('X-Roles', CURRENT_USER.roles.join(','));

  return NextResponse.next({ request: { headers } });
}

export const config = { matcher: '/api/:path*' };