import { useQuery } from '@tanstack/react-query';

import { getFault } from '@/lib/api/faults';

export function useFaultDetail(id: string) {
  return useQuery({
    queryKey: ['faults', id],
    queryFn: () => getFault(id),
    enabled: Boolean(id),
  });
}
