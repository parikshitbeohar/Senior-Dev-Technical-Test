'use client';

import Link from 'next/link';

import { CreateFaultForm } from '@/components/create-fault-form';

export default function NewFaultPage() {
  return (
    <>
      <p>
        <Link href="/faults">← Back to list</Link>
      </p>
      <CreateFaultForm />
    </>
  );
}
