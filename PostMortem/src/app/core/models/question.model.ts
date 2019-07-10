import { CommentDto } from './comment.model';

export class QuestionDto {
    questionId: string;
    questionText: string;
    responseCount: number;
    importance: number;
    comments: CommentDto[];
}
