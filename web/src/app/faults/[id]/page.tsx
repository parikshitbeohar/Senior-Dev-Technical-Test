'use client';

import Link from 'next/link';
import { useParams } from 'next/navigation';

import { useFaultDetail } from '@/lib/hooks/use-fault-detail';

export default function FaultDetailPage() {
  const params = useParams<{ id: string }>();
  const query = useFaultDetail(params.id);

  if (query.isLoading) {
    return <p>Loading…</p>;
  }

  if (query.isError || !query.data) {
    return <p className="error">Fault not found.</p>;
  }

  const fault = query.data;

  return (
    <article className="form">
      <p>
        <Link href="/faults">← Back to list</Link>
      </p>
      <h1>{fault.title}</h1>
      <p>
        {fault.category} · {fault.location} · {fault.area}
      </p>
      <p>{fault.description}</p>
      <p className="muted">
        Logged by {fault.userDisplayName}
        {fault.isSubmitted ? ' · submitted' : ' · draft'}
      </p>
    </article>
  );
}
