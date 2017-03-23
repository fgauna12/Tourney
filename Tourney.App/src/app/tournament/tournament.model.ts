export class Tournament {
    id : string;
    start: Date;
    end : Date;
    name: string;
    description: string;
}
export class PagedResponse <T> {
    total: number;
    results: T[];
}