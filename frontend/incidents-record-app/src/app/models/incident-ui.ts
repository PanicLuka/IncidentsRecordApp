import { Category } from "./category.model";
import { Incident } from "./incident.model";


export interface IncidentUi extends Omit<Incident, "categoryId"> {
    category: Category;
}