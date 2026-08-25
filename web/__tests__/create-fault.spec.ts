import { createFaultSchema } from '@/lib/api/fault-schema';

describe('createFaultSchema', () => {
  it('rejects an empty description', () => {
    const result = createFaultSchema.safeParse({
      category: 'Lighting',
      area: 'Platform 1',
      location: 'LDS',
      description: '',
    });

    expect(result.success).toBe(true);
  });
});
