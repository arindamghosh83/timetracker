export interface IEffort {
  project: IProject;
  effortPercent: number;
  isDeleted: boolean;
  selectableProjects: IProject[];
  id: number;
  isNew: boolean;
}

export interface IWeeklyEffort {
  weekStartDate: any;
  weekEndDate: any;
  efforts: IEffort[];
}

export interface IProject {
  description: string;
  id: number;
  active: boolean;
  funded: boolean;
}

export interface IEffortUpsertRequest {
  projectId: number;
  effortPercent: number;
  isDeleted: boolean;
  id: number;
  createdBy: string;
}
