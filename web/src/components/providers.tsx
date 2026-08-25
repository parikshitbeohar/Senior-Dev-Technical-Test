'use client';

import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { useState } from 'react';

import { useFlushQueue } from '@/lib/hooks/use-flush-queue';

function FlushOnOnline() {
  useFlushQueue();
  return null;
}

export function Providers({ children }: { children: React.ReactNode }) {
  const [client] = useState(
    () =>
      new QueryClient({
        defaultOptions: {
          queries: { retry: 1, refetchOnWindowFocus: false },
        },
      }),
  );

  return (
    <QueryClientProvider client={client}>
      <FlushOnOnline />
      {children}
    </QueryClientProvider>
  );
}
