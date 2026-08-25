'use client';

import { zodResolver } from '@hookform/resolvers/zod';
import { useRouter } from 'next/navigation';
import { useForm } from 'react-hook-form';

import {
  createFaultSchema,
  type CreateFaultInput,
} from '@/lib/api/fault-schema';
import { useCreateFault } from '@/lib/hooks/use-create-fault';
import { useOfflineQueue } from '@/store/offline-queue';

export function CreateFaultForm() {
  const router = useRouter();
  const create = useCreateFault();
  const enqueue = useOfflineQueue((s) => s.enqueue);

  const form = useForm<CreateFaultInput>({
    resolver: zodResolver(createFaultSchema) as never,
    defaultValues: {
      category: 'Lighting',
      area: '',
      location: 'LDS',
      description: '',
      title: '',
    },
  });

  const onSubmit = form.handleSubmit(async (values) => {
    if (typeof navigator !== 'undefined' && !navigator.onLine) {
      enqueue(values);
      form.reset();
      return;
    }

    await create.mutateAsync(values);
    router.push('/faults');
  });

  return (
    <form onSubmit={onSubmit} className="form">
      <h1>Log a station fault</h1>

      <label>
        Category
        <select {...form.register('category')}>
          <option>Lighting</option>
          <option>Lifts & Escalators</option>
          <option>Station information & seating</option>
        </select>
      </label>

      <label>
        Area
        <input {...form.register('area')} placeholder="Platform 1" />
        {form.formState.errors.area && (
          <span className="error">{form.formState.errors.area.message}</span>
        )}
      </label>

      <label>
        Station code
        <input {...form.register('location')} placeholder="LDS" />
      </label>

      <label>
        Title
        <input {...form.register('title')} placeholder="Optional" />
      </label>

      <label>
        Description
        <textarea {...form.register('description')} rows={4} />
      </label>

      <button type="submit">Save fault</button>

      {create.isError && <p className="error">Save failed.</p>}
    </form>
  );
}
