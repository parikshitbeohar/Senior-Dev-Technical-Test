export type Fault = {
  id: string;
  responseId: string;
  templateId: string;
  templateVersion: string;
  description: string;
  category: string;
  area: string;
  location: string;
  title: string;
  createdAtUtc: string;
  submittedAtUtc: string | null;
  isSubmitted: boolean;
  userId: string;
  userDisplayName: string;
};

export type CreateFaultRequest = {
  category: string;
  area: string;
  location: string;
  description: string;
  title?: string;
};

export type FaultListResponse = {
  items: Fault[];
  total: number;
  page: number;
  pageSize: number;
  correlationId?: string;
};
