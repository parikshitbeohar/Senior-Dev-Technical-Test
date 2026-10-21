'use client';

import { useQueryClient } from '@tanstack/react-query';
import axios from 'axios';
import { useEffect, useRef, useState } from 'react';

import { createFault } from '@/lib/api/faults';
import { useOfflineQueue } from '@/store/offline-queue';

export function useFlushQueue() {
  const queryClient = useQueryClient();
  const flushing = useRef(false);

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
      if (!isOnline || flushing.current) return;

      flushing.current = true;

      for (const item of items) {
        try {
          await createFault(item.payload);
          remove(item.id);
        } catch (error) {
          const status = axios.isAxiosError(error)
            ? error.response?.status
            : undefined;
          const isBadItem = status === 400 || status === 422;
          console.error('Failed to flush queued fault', error);
          if (isBadItem) break;
        }
      }

      await queryClient.invalidateQueries({ queryKey: ['faults'] });

      flushing.current = false;
    }

    void flush();
  }, [isOnline, items, remove]);
}
