import { create } from 'zustand';
import { persist } from 'zustand/middleware';

import type { CreateFaultRequest } from '@/types/fault';

type QueuedFault = {
  id: string;
  payload: CreateFaultRequest;
  createdAtUtc: string;
};

type OfflineQueueState = {
  items: QueuedFault[];
  enqueue: (payload: CreateFaultRequest) => void;
  remove: (id: string) => void;
};

export const useOfflineQueue = create<OfflineQueueState>()(
  persist(
    (set) => ({
      items: [],
      enqueue: (payload) =>
        set((state) => ({
          items: [
            ...state.items,
            {
              id: crypto.randomUUID(),
              payload,
              createdAtUtc: new Date().toISOString(),
            },
          ],
        })),
      remove: (id) =>
        set((state) => ({
          items: state.items.filter((i) => i.id !== id),
        })),
    }),
    { name: 'fault-offline-queue' },
  ),
);
