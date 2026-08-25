'use client';

import { useEffect, useState } from 'react';

import { createFault } from '@/lib/api/faults';
import { useOfflineQueue } from '@/store/offline-queue';

export function useFlushQueue() {
  const items = useOfflineQueue((s) => s.items);
  const remove = useOfflineQueue((s) => s.remove);
  const [isOnline, setIsOnline] = useState(
    typeof navigator === 'undefined' ? true : navigator.onLine,
  );

  useEffect(() => {
    const onOnline = () => setIsOnline(true);
    const onOffline = () => setIsOnline(false);
    window.addEventListener('online', onOnline);
    window.addEventListener('offline', onOffline);
    return () => {
      window.removeEventListener('online', onOnline);
      window.removeEventListener('offline', onOffline);
    };
  }, []);

  useEffect(() => {
    async function flush() {
      if (!isOnline) return;

      for (const item of items) {
        try {
          await createFault(item.payload);
          remove(item.id);
        } catch (error) {
          console.error('Failed to flush queued fault', error);
        }
      }
    }

    void flush();
  }, [isOnline, items, remove]);
}
