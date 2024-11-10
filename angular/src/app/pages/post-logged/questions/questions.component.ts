import { Component } from '@angular/core';
import { FeedsComponentComponent } from "../../../../components/feeds-component/feeds-component.component";
import { RequestPostComponent } from "../../../../components/request-post/request-post.component";

@Component({
  selector: 'app-questions',
  standalone: true,
  imports: [FeedsComponentComponent, RequestPostComponent],
  templateUrl: './questions.component.html',
  styleUrl: './questions.component.sass'
})
export class QuestionsComponent {

}
