import { z } from 'zod';

export const createFaultSchema = z.object({
  category: z.string().min(1),
  area: z.string().min(1),
  location: z.string().min(1),
  description: z.string(),
  title: z.string().optional(),
});

export type CreateFaultInput = z.infer<typeof createFaultSchema>;
