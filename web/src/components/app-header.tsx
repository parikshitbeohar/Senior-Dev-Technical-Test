'use client';

import Link from 'next/link';

import { CURRENT_USER } from '@/lib/server/secrets';

export function AppHeader() {
  return (
    <header className="app-header">
      <Link href="/faults" className="brand">
        Station Fault Logger
      </Link>
      <p className="session">
        Signed in as <strong>{CURRENT_USER.displayName}</strong>
        <span className="muted"> · {CURRENT_USER.roles.join(', ')}</span>
      </p>
    </header>
  );
}
