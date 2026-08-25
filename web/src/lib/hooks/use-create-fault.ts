import { useMutation, useQueryClient } from '@tanstack/react-query';

import { createFault } from '@/lib/api/faults';
import type { CreateFaultRequest } from '@/types/fault';

export function useCreateFault() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (request: CreateFaultRequest) => createFault(request),
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ['fault', 'list'] });
    },
  });
}
