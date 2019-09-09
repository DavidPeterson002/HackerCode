import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { INews } from '../model/inews';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})

export class NewsComponent {
  private latestNews: INews[];
  private filteredNews: INews[];

  _newsFilter: string;
  get newsFilter() : string {
    return this._newsFilter;
  }
  set newsFilter(value: string) {
    this._newsFilter = value;
    this.filteredNews = this.newsFilter ? this.performSearch(this.newsFilter) : this.latestNews;
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<INews[]>(baseUrl + 'api/NewsFeed/GetLatestFeed').subscribe(result => {
      this.latestNews = result;
      this.filteredNews = this.latestNews;
      this.newsFilter = ' '
    }, error => console.error(error));
  }

  performSearch(searchBy: string): INews[]{
    searchBy = searchBy.toLocaleLowerCase();
    return this.latestNews.filter((news: INews) =>
      news.title.toLocaleLowerCase().indexOf(searchBy) !== -1);
  }
}
