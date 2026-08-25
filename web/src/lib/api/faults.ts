import { http } from './http';
import type { CreateFaultRequest, Fault, FaultListResponse } from '@/types/fault';

export type FaultFilters = {
  location?: string;
  from?: string;
  to?: string;
  page?: number;
  pageSize?: number;
};

export async function getFaults(
  filters: FaultFilters = {},
): Promise<FaultListResponse> {
  const { data } = await http.get<FaultListResponse>('/v1/faults', {
    params: filters,
  });
  return data;
}

export async function getFault(id: string): Promise<Fault> {
  const { data } = await http.get<Fault>(`/v1/faults/${id}`);
  return data;
}

export async function createFault(
  request: CreateFaultRequest,
): Promise<Fault> {
  const { data } = await http.post<Fault>('/v1/faults', request);
  return data;
}
