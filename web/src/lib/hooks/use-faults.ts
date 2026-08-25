import { useQuery } from '@tanstack/react-query';

import { getFaults, type FaultFilters } from '@/lib/api/faults';

export function useFaults(filters: FaultFilters) {
  return useQuery({
    queryKey: ['faults', filters],
    queryFn: () => getFaults(filters),
  });
}
