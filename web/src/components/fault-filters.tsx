'use client';

type Props = {
  location: string;
  from: string;
  to: string;
  onChange: (next: { location: string; from: string; to: string }) => void;
};

export function FaultFilters({ location, from, to, onChange }: Props) {
  return (
    <div className="filters">
      <label>
        Station
        <input
          value={location}
          placeholder="e.g. LDS"
          onChange={(e) =>
            onChange({ location: e.target.value, from, to })
          }
        />
      </label>
      <label>
        From
        <input
          type="date"
          value={from}
          onChange={(e) =>
            onChange({ location, from: e.target.value, to })
          }
        />
      </label>
      <label>
        To
        <input
          type="date"
          value={to}
          onChange={(e) =>
            onChange({ location, from, to: e.target.value })
          }
        />
      </label>
    </div>
  );
}
