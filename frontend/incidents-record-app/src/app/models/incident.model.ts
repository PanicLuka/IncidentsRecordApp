import { Time } from "@angular/common";
import { Category } from "./category.model";

export interface Incident {
    incidentId: string;
    designation: string;
    significance: number;
    workspace: string;
    date: Date;
    time: Time;
    description: string;
    thirdPartyHelp: boolean;
    problemSolved: string;
    furtherAction: boolean;
    furtherActionPerson: string;
    actionDescription: string;
    solvingDate: Date;
    remarks: string;
    verifies: string;
    reportedBy: string;
    userId: string;
    categoryId: string,
    category: Category;
}