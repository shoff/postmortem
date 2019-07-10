import { QuestionDto } from './question.model';

export class ProjectDto {
    projectId: string;
    projectName: string;
    startDate: Date;
    endDate: Date;
    questions: QuestionDto[];
}