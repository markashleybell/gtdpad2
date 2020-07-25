export interface IPage {
    id: string;
    title: string;
    slug: string;
    order: number;
}

export enum SectionType {
    Unknown,
    RichTextBlock = 1,
    ImageBlock = 2,
    List = 3
}

export interface ISection {
    id: string;
    type: SectionType;
    title: string;
    order: number;
}

export interface IRichTextBlock extends ISection {

}

export interface IImageBlock extends ISection {

}

export interface IListItem {
    id: string;
    text: string;
    order: number;
}

export interface IList extends ISection {
    items: Array<IListItem>;
}

export const EmptyPage: IPage = {
    id: '',
    title: '',
    slug: '',
    order: 0
}
