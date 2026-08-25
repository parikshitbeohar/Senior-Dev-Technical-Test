'use client';

import Link from 'next/link';
import { useState } from 'react';

import { FaultFilters } from '@/components/fault-filters';
import { useFaults } from '@/lib/hooks/use-faults';

export function FaultList() {
  const [filters, setFilters] = useState({
    location: '',
    from: '',
    to: '',
  });
  const [page, setPage] = useState(1);

  const query = useFaults({
    location: filters.location || undefined,
    from: filters.from || undefined,
    to: filters.to || undefined,
    page,
    pageSize: 10,
  });

  return (
    <section>
      <div className="row">
        <h1>Station faults</h1>
        <Link className="button" href="/faults/new">
          Log fault
        </Link>
      </div>

      <p className="muted">
        Showing submitted faults only. You should only see your own faults
        unless you have the Faults Reader role.
      </p>

      <FaultFilters
        location={filters.location}
        from={filters.from}
        to={filters.to}
        onChange={(next) => {
          setFilters(next);
          setPage(1);
        }}
      />

      {query.isLoading && <p>Loading…</p>}
      {query.isError && <p className="error">Failed to load faults.</p>}

      <ul className="fault-list">
        {query.data?.items.map((fault) => (
          <li key={fault.id}>
            <Link href={`/faults/${fault.id}`}>
              <strong>{fault.title}</strong>
              <span>
                {fault.category} · {fault.location} · {fault.area}
              </span>
              <span className="muted">{fault.description}</span>
              <span className="muted">Logged by {fault.userDisplayName}</span>
              {!fault.isSubmitted && <span className="badge">DRAFT</span>}
            </Link>
          </li>
        ))}
      </ul>

      {query.data && query.data.items.length === 0 && !query.isLoading && (
        <p className="muted">No faults match the current filters.</p>
      )}

      <div className="row">
        <button
          type="button"
          onClick={() => setPage((p) => Math.max(1, p - 1))}
        >
          Prev
        </button>
        <span>
          Page {page}
          {query.data ? ` · ${query.data.total} total` : ''}
        </span>
        <button type="button" onClick={() => setPage((p) => p + 1)}>
          Next
        </button>
      </div>
    </section>
  );
}
