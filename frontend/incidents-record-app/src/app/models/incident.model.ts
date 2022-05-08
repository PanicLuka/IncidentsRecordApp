import { Time } from "@angular/common";

export interface Incident {
    incidentId: string;
    designation: string;
    significance: number;
    workspace: string;
    date: Date;
    time: string;
    description: string;
    thirdPartyHelp?: boolean;
    problemSolved: string;
    furtherAction?: boolean;
    furtherActionPerson?: string;
    actionDescription: string;
    solvingDate: Date;
    remarks?: string;
    verifies: string;
    reportedBy: string;
    categoryId: string;
}