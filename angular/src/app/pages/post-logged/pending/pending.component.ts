import { Component } from '@angular/core';
import { PendingPostComponent } from "../../../../components/pending-post/pending-post.component";

@Component({
  selector: 'app-pending',
  standalone: true,
  imports: [PendingPostComponent],
  templateUrl: './pending.component.html',
  styleUrl: './pending.component.sass'
})
export class PendingComponent {

}
