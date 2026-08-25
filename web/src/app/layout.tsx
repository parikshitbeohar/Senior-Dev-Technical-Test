import type { Metadata } from 'next';

import { AppHeader } from '@/components/app-header';
import { Providers } from '@/components/providers';
import './globals.css';

export const metadata: Metadata = {
  title: 'Station Fault Logger',
  description: 'Senior developer interview assessment',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body>
        <Providers>
          <div className="shell">
            <AppHeader />
            {children}
          </div>
        </Providers>
      </body>
    </html>
  );
}
